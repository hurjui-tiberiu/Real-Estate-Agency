using Internship_2022.Application.Interfaces;
using Internship_2022.Infrastructure.Interfaces;
using Quartz;
using Quartz.Spi;

class SendReminderEmailJobFactory : IJobFactory
{
    protected readonly IServiceProvider _container;

    public SendReminderEmailJobFactory(IServiceProvider container)
    {
        _container = container;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {

        var mailService = _container.GetService<IMailService>()!;
        var listingRepository = _container.GetService<IListingRepository>()!;
        var userService=_container.GetService<IUserService>()!;
        return new SendReminderEmailJob(mailService, listingRepository, userService);
    }

    public void ReturnJob(IJob job)
    {
        (job as IDisposable)?.Dispose();
    }
}