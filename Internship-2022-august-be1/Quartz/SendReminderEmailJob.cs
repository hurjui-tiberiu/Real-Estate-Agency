using Internship_2022.Application.Interfaces;
using Internship_2022.Application.Models.MailDto;
using Internship_2022.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Diagnostics;

public class SendReminderEmailJob : IJob
{
    private readonly IMailService mailService;
    private readonly IListingRepository listingRepository;
    private readonly IUserService userService;

    private const string EmailSubject = "Listings that need approval";

    public SendReminderEmailJob(IMailService mailService, IListingRepository listingRepository, IUserService userService)
    {
        this.mailService = mailService;
        this.listingRepository = listingRepository;
        this.userService = userService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var listings = await listingRepository.GetListingAsync();

        if (listings.Count == 0)
            return;

        string MailInfo = "<b> Some listings need validation: </b> <br/>";
        string ListingsInfo = string.Empty;
        TimeSpan ts = new TimeSpan();

        foreach (var listing in listings)
        {
            if (listing.Status == true)
                ts = DateTime.Now - listing.CreatedUtc;
              //  if(ts.TotalHours)
                    ListingsInfo += "Title: " + listing.Title + "<br/>";
        }
        MailInfo += ListingsInfo;

        var admins = await userService.GetAdminsAsync();
        foreach (var user in admins)
        {
             //await mailService.SendEmailAsync(new MailRequest { ToEmail = "atiberiu84@gmail.com", Subject = EmailSubject, Body = MailInfo });
        }
    }
}