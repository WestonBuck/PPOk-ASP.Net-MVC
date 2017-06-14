using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Clients;
using PPOK_System.Domain.Service;
using PPOK_System.Domain.Models;
using PPOK_System.Models;
using Hangfire;
using System.Linq;

namespace PPOK_System.TwilioManager
{
    public class TwManager
    {
        Database db = new Database(SystemContext.DefaultConnectionString);
        public void StartHangfire()
        {
            RecurringJob.AddOrUpdate(() => ScheduleSend(), Cron.Daily);
        }
        public void call()
        {
            string AccountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            string AuthToken = "ACc4455ec9d784ae580638ecac36ad7fea";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            CallResource.Create(
                from: new PhoneNumber("14054000298"),
                to: new PhoneNumber("14059191824"),
                url: new Uri("~/Twilio/TwillioXML/PickUp.xml"));
        }

        public void SendResponse(string phonenum, string msg)
        {
            const string accountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            const string authToken = "2a6f621c0a94b7e60c906e06e15966fb";

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);
            
            MessageResource.Create(
                from: new PhoneNumber("405-400-0298"),
                to: new PhoneNumber(phonenum),

                body: $"{msg}");
            
        }
        public void SendNotifications(List<Schedule> scheduleList)
        {
            const string accountSid = "ACc4455ec9d784ae580638ecac36ad7fea";
            const string authToken = "2a6f621c0a94b7e60c906e06e15966fb";

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);

            var people = new Dictionary<string, string>();
            foreach (Schedule s in scheduleList)
            {
                Message msg = new Message();
                msg.prescription_id = s.prescription_id;
                msg.fill_time = DateTime.Now;
                db.Create(msg);
                MessageResource.Create(
                    from: new PhoneNumber("405-400-0298"),
                    to: new PhoneNumber(s.person.phone),

                    body: $"{s.person.first_name}, " + "Would you like to refill your perscription? Send '1' for refill");
            }
        }
        public void RefillRequest(string num)
        {
            string number = num.Substring(2);
            Message topMsg = new Message();
            DateTime lowTime = new DateTime(1753, 1, 1);
            topMsg.fill_time = lowTime;
            Person person = db.ReadSinglePersonByPhone(number);
            List<Message> messages = db.ReadAllMessagesForPerson(person.person_id);
            foreach (Message m in messages)
            {
                if (DateTime.Compare(topMsg.fill_time, m.fill_time) < 0)
                    topMsg = m;
            }
            topMsg.response = "yes";
            topMsg.pick_up_time = null;
            db.Update(topMsg);
        }
        public void ScheduleSend()
        {
            List<Schedule> schedules = db.GetSchedules();
            if (schedules.Count > 0)
                SendNotifications(schedules);
        }
    }
}