using PPOK_System.Domain.Models;

namespace PPOK_System.Service.Models {
	public class UserPrincipalSerialize : IUserPrincipal {
		public UserPrincipalSerialize() : base() {}
		public UserPrincipalSerialize(string email) : base() {
			Email = email;
		}

		public UserPrincipalSerialize(Person person) : base() {
			Id = person.person_id;
			FirstName = person.first_name;
			LastName = person.last_name;
			Phone = person.phone;
			Email = person.email;
			Store = person.store;
			setRole(person.person_type);
		}
	}
}