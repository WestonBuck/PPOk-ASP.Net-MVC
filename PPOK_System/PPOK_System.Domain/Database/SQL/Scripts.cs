using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PPOK_System.Domain.Database.SQL {
	public class Scripts {
		public static Dictionary<string, string> ScriptDictionary { get; private set; }
		public static Dictionary<string, string> Create { get; private set; }
		public static Dictionary<string, string> Read { get; private set; }
		public static Dictionary<string, string> Update { get; private set; }
		public static Dictionary<string, string> Delete { get; private set; }

		public static string CreateDatabaseSql { get; private set; }
		public static string CreateTablesSql { get; private set; }
		public static string InsertDummyDataSql { get; private set; }

		static Scripts() {
			PopulateInstantiationScripts();
			PopulateCreateScripts();
			PopulateReadScripts();
			PopulateUpdateScripts();
			PopulateDeleteScripts();
		}


		// Instantiation Scripts
		private static void PopulateInstantiationScripts() {
			string SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL/Instantiation";
			ScriptDictionary = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);

			CreateDatabaseSql = ScriptDictionary["CreateDatabase"];
			CreateTablesSql = ScriptDictionary["CreateTables"];
			InsertDummyDataSql = ScriptDictionary["InsertDummyData"];
		}


		// Create Scripts
		private static void PopulateCreateScripts() {
			string SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL/Create";
			Create = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}


		// Read Scripts
		private static void PopulateReadScripts() {
			string SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL/Read";
			Read = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}


		// Update Scripts
		private static void PopulateUpdateScripts() {
			string SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL/Update";
			Update = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}


		// Delete Scripts
		private static void PopulateDeleteScripts() {
			string SqlRoot = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "/PPOK_System.Domain/Database/SQL/Delete";
			Delete = Directory.GetFiles(SqlRoot, "*.sql").ToDictionary(Path.GetFileNameWithoutExtension, File.ReadAllText, StringComparer.OrdinalIgnoreCase);
		}
	}
}