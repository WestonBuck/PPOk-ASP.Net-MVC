using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PPOK_System.Domain.Database.SQL;
using PPOK_System.Domain.Models;
using PPOK_System.Domain.Service.Cryptography;
using System;

namespace PPOK_System.Domain.Service {
	public class Database {
		private readonly string connection;
		private readonly string _master;

		public Database(string defaultConnection, string master=null) {
			connection = defaultConnection;
			_master = master;
		}


		#region Database Creation

		// Initialize the Database
		public void initDatabase() {
			CreateDatabase();
			CreateTables();
			InsertData();
		}

		// Drop & Create Database
		private void CreateDatabase() {
			using (IDbConnection db = new SqlConnection(_master)) {
				db.Execute(Scripts.CreateDatabaseSql);
			}
		}


		// Create Tables to fill the Database
		private void CreateTables() {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.CreateTablesSql);
			}
		}


		// Fill tables with dummy data
		private void InsertData() {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.InsertDummyDataSql);
			}
		}

		#endregion


		#region Create

		// Create new row in "store" table
		public void Create(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Store"], s);
			}
		}


		// Create new row in "person" table
		public void Create(Person p) {
			if (!p.person_id.HasValue)
				p.person_id = GenerateId<Person>();
			if (p.password != null)
				p.password = Encrypt.Encode(p.password);
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Person"], p);
				GenerateContact(p.person_id);
			}
		}


		// Create new row in "prescription" table
		public void Create(Prescription p) {
			if (!p.prescription_id.HasValue)
				p.prescription_id = GenerateId<Prescription>();
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Prescription"], p);
			}
		}


		// Create new row in "drug" table
		public void Create(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Drug"], d);
			}
		}


		// Create new row in "message" table
		public void Create(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Message"], m);
			}
		}


		// Create new row in "contact_preference" table
		public void Create(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["ContactPreference"], c);
			}
		}


		// Create new row in "contact_preference" table
		public void Create(Schedule s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Create["Schedule"], s);
			}
		}


		#endregion


		#region Read

		// Populate List<Store> with rows in the Db
		public List<Store> ReadAllStores() {
			var lookup = new Dictionary<int, Store>();

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = Scripts.Read["AllStores"];
				var result = db.Query<Store, Person, Store>(sql,
					(s, p) => {
						Store store;
						if (!lookup.TryGetValue(s.store_id.Value, out store)) { 
							store = s;
							lookup.Add(s.store_id.Value, store);
						}

						if (store.pharmacists == null)
							store.pharmacists = new List<Person>();

						store.pharmacists.Add(p);

						return store;
					},
					splitOn: "store_id,person_id").AsList();
			}

			return lookup.Values.ToList();
		}


		// Populate single Store with row in the Db
		public Store ReadSingleStore(int id) {
			Store store = null;

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = Scripts.Read["SingleStore"];
				var result = db.Query<Store, Person, Person>(sql,
					(s, p) => {
						if (store == null)
							store = s;

						p.store = s;
						return p;
					}, new { param = id },
					splitOn: "store_id,person_id").AsList();
				if (store != null) {
					store.pharmacists = result;
				}
			}

			return store;
		}


		// Populate List<Drug> with rows in the Db
		public List<Drug> ReadAllDrugs() {
			using (IDbConnection db = new SqlConnection(connection)) {
				return db.Query<Drug>(Scripts.Read["AllDrugs"]).ToList();
			}
		}


		// Populate single Drug with row in the Db
		public Drug ReadSingleDrug(string id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				return db.Query<Drug>(Scripts.Read["SingleDrug"], new { param = id }).FirstOrDefault();
			}
		}

		// Populate List<Prescriptions> with rows in the Db
		public List<Prescription> ReadAllPrescriptions() {
			return QueryPrescriptionList(Scripts.Read["AllPrescriptions"]);
		}


		// Populate List<Prescriptions> with rows in the Db based off person_id
		public List<Prescription> ReadAllPrescriptionsForPerson(int? id) {
			return QueryPrescriptionList(Scripts.Read["AllPrescriptionsForPerson"], id);
		}


		// Populate single Prescriptions with row in the Db
		public Prescription ReadSinglePrescription(int? id) {
			return QueryPrescriptionSingle(Scripts.Read["SinglePrescriptionById"], id);
		}


		// Populate single Prescriptions with row in the Db
		public Prescription ReadSinglePrescription(int? person_id, string drug_id) {
			return QueryPrescriptionSingle(Scripts.Read["SinglePrescriptionByDrug"], person_id, drug_id);
		}


		private Prescription QueryPrescriptionSingle(string sql, int? param, string param2="") {
			using (IDbConnection db = new SqlConnection(connection)) {
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { param = param, param2 = param2 },
					splitOn: "prescription_id,person_id,drug_id").FirstOrDefault();
				return result;
			}
		}


		private List<Prescription> QueryPrescriptionList(string sql, int? param=null) {
			using (IDbConnection db = new SqlConnection(connection)) {
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { param = param },
					splitOn: "prescription_id,person_id,drug_id").AsList();
				return result;
			}
		}


		// Populate List<Prescription> with row in the Db based off date(s)
		public List<Prescription> ReadAllPrescriptionsForDates(string drug_id, DateTime begin, DateTime end) {
			string sql = Scripts.Read["AllPrescriptionsForDates"];
			using (IDbConnection db = new SqlConnection(connection)) {
				var result = db.Query<Prescription, Person, Drug, Prescription>(sql,
					(p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;

						return p1;
					}, new { param = drug_id, param2 = begin, param3 = end },
					splitOn: "prescription_id,person_id,drug_id").AsList();
				return result;
			}
		}


		// Populate List<Message> with rows in the Db
		public List<Message> ReadAllMessages() {
			return QueryAllMessages(Scripts.Read["AllMessages"]);
		}


		// Populate List<Message> with rows in the Db
		public List<Message> ReadAllMessagesForPerson(int? id) {
			return QueryAllMessages(Scripts.Read["AllMessagesForPerson"], id);
		}


		private List<Message> QueryAllMessages(string sql, int? param=null) {
			using (IDbConnection db = new SqlConnection(connection)) {
				var result = db.Query<Message, Prescription, Person, Drug, Message>(sql,
					(m, p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;
						m.prescription = p1;

						return m;
					}, new { param = param },
					splitOn: "message_id,prescription_id,person_id,drug_id").AsList();

				return result;
			}
		}


		// Populate single Message with row in the Db
		public Message ReadSingleMessage(int id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = Scripts.Read["SingleMessage"];
				var result = db.Query<Message, Prescription, Person, Drug, Message>(sql,
					(m, p1, p2, d) => {
						p1.customer = p2;
						p1.drug = d;
						m.prescription = p1;

						return m;
					}, new { param = id },
					splitOn: "message_id,prescription_id,person_id,drug_id").FirstOrDefault();

				return result;
			}
		}


		// Populate List<Person> with row in the Db
		public List<Person> ReadAllPersons() {
			var lookup = new Dictionary<int, Person>();

			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = Scripts.Read["AllPersons"];
				var result = db.Query<Person, Store, ContactPreference, Person>(sql,
					(p, s, c) => {
						Person person;
						if (!lookup.TryGetValue(p.person_id.Value, out person))
							lookup.Add(p.person_id.Value, person = p);

						if (person.store == null)
							person.store = s;

						if (person.contact_preference == null)
							person.contact_preference = c;

						return p;
					},
					splitOn: "person_id,store_id,preference_id").AsList();

				return lookup.Values.ToList();
			}
		}
		

		// Populate Single Person with all Store and ContactPreferences tied to it based off person_id
		public Person ReadSinglePerson(int id) {
			return QueryPerson(Scripts.Read["SinglePersonById"], id.ToString());
		}


		// Populate Single Person with all Store and ContactPreferences tied to it based off email
		public Person ReadSinglePerson(string email) {
			return QueryPerson(Scripts.Read["SinglePersonByEmail"], email);
		}


		// Populate Single Person with all Store and ContactPreferences tied to it based off phone number
		public Person ReadSinglePersonByPhone(string phone) {
			return QueryPerson(Scripts.Read["SinglePersonByPhone"], phone);
		}


		// Used to handle ReadSinglePerson by any paramater value
		private Person QueryPerson(string sql, string param) {
			Person person = null;

			using (IDbConnection db = new SqlConnection(connection)) {
				var result = db.Query<Person, Store, ContactPreference, Person>(sql, (p, s, c) => {
					if (person == null) {
						person = p;
						person.store = s;
					}

					if (person.contact_preference == null)
						person.contact_preference = c;

					return p;
				}, new { param = param },
				splitOn: "person_id,store_id,preference_id").FirstOrDefault();
			}

			return person;
		}


		// Read and get items from scheduler for todays date
		public List<Schedule> GetSchedules() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string sql = Scripts.Read["Schedules"];
				var result = db.Query<Schedule, Prescription, Person, Schedule>(sql,
					(s, p, pers) => {
						s.person = pers;
						return s;
					},
					splitOn: "prescription_id,person_id").AsList();
				return result;
			}
		}




		#endregion


		#region Update

		// Update row in "store" table
		public void Update(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["Store"], s);
			}
		}


		// Update row in "person" table
		public void Update(Person p) {
			p.password = Encrypt.Encode(p.password);
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["Person"], p);
			}
		}


		// Update row in "person" table
		public void Update(Prescription p) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["Prescription"], p);
			}
		}


		// Update row in "drug" table
		public void Update(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["Drug"], d);
			}
		}


		// Update row in "message_history" table
		public void Update(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["Message"], m);
			}
		}


		// Update row in "contact_preference" table
		public void Update(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Update["ContactPreference"], c);
			}
		}
		
		#endregion


		#region Delete

		// Delete row in "store" table
		public void Delete(Store s) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["Store"], s);
			}
		}


		// Delete row in "person" table
		public void Delete(Person p) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["Person"], p);
			}
		}


		// Delete row in "prescription" table
		public void Delete(Prescription p) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["Prescription"], p);
			}
		}


		// Delete row in "drug" table
		public void Delete(Drug d) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["Drug"], d);
			}
		}


		// Delete row in "message_history" table
		public void Delete(Message m) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["Message"], m);
			}
		}


		// Delete row in "contact_preference" table
		public void Delete(ContactPreference c) {
			using (IDbConnection db = new SqlConnection(connection)) {
				db.Execute(Scripts.Delete["ContactPreference"], c);
			}
		}

		#endregion


		#region Misc

		// Generate ID Number
		public int GenerateId<T>() {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.Query<int>($"SELECT Max([{name}_id]) FROM [{name}]").First() + 1;
			}
		}


		// See if T exists in Database by int id
		public bool Exists<T>(int? id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.ExecuteScalar<bool>($"SELECT COUNT(1) FROM {name} WHERE {name}_id=@id", new { id });
			}
		}


		// See if T exists in Database by string id
		public bool Exists<T>(string id) {
			using (IDbConnection db = new SqlConnection(connection)) {
				string name = typeof(T).Name.ToLower();
				return db.ExecuteScalar<bool>($"SELECT COUNT(1) FROM {name} WHERE {name}_id=@id", new { id });
			}
		}


		private ContactPreference GenerateContact(int? id) {
			var contact = new ContactPreference();
			contact.person_id = id;
			contact.contact_type = "Phone";
			Create(contact);
			return contact;
		}

		#endregion
	}
}
