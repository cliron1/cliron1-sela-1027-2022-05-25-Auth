namespace Identity.Data.Entities {
	public class MyUser {
		public int Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string Email { get; set; }
		public string Pwd { get; set; }
		public DateTime Created { get; set; }
	}
}
