using System;

// Represents a task that HangFire will call in order to send the message to a given person
namespace PPOK_System.Domain.Models {
    public class Schedule {
        public int task_id { get; set; }
        public int prescription_id { get; set; }
        public string response { get; set; }
        public DateTime day_to_send { get; set; }

        public Person person { get; set; }
	}
}