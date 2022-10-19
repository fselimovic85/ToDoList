using Devart.Data.PostgreSql;
using Microsoft.Data.SqlClient;
using Quartz;
using System.Data;
using System.Runtime;
using TO_DO_LIST.Models;
using TO_DO_LIST.Service.MailSenderService;

namespace TO_DO_LIST.Service.CroneJobService
{
    public class Crone_Job_Service : IJob
    {
        private IEmailSender _emailSender;
        private IConfiguration _configuration;

        public Crone_Job_Service(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                List<Tasks> DailyTaskList = new List<Tasks>();
                using (SqlConnection dataConnection = new SqlConnection(_configuration.GetSection("Pagination:DefaultConnection").Value))
                {
                    dataConnection.Open();
                    SqlCommand DailyTaskUserId = new SqlCommand();

                    SqlCommand commandIdUser = new SqlCommand();
                    commandIdUser.Connection = dataConnection;
                    commandIdUser.CommandText = " SELECT Id, TimeZone, Email " +
                                                " From User ";

                    var readerUserId = commandIdUser.ExecuteReader();
                    

                    if(readerUserId != null)
                    {
                        while (readerUserId.Read())
                        {
                            DailyTaskUserId.Connection = dataConnection;
                            DailyTaskUserId.CommandText = " select t.Title, t.Description, t.Deadline, t.Done, t.AddedToDone " +
                                                          " from Task as t " +
                                                          " left outer join DailyList as d on t.DailyListId = d.Id " +
                                                          " where d.UserId =" + readerUserId["Id"];

                            var readerTaskUserId=DailyTaskUserId.ExecuteReader();

                             while(readerTaskUserId.Read())
                            {
                                DailyTaskList.Add(new Tasks
                                {
                                    Title = readerTaskUserId["Title"].ToString(),
                                    Description = readerTaskUserId["Description"].ToString(),
                                    Deadline = Convert.ToDateTime(readerTaskUserId["DeadLine"]),
                                    Done =bool.Parse(readerTaskUserId["Done"].ToString()),
                                    AddedToDone = Convert.ToDateTime(readerTaskUserId["AddedToDone"])
                                });

                            }
                            var message = new Message(new List<string>()
                                {
                                    readerTaskUserId["Email"].ToString()
                                },
                                       "Daily list for:" + DateTime.UtcNow,
                                       "This is daily done list:" + DailyTaskList.ToArray().ToString()); 

                                             _emailSender.SendEmail(message);

                        }
                    }

                    
                }
            }
            catch
            {
                throw;
            }

            return Task.CompletedTask;
        }
    }
}
