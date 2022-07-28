using Defender.UserManagement.Application.Common.Interfaces;

namespace Defender.UserManagement.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
