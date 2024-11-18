using FluentValidation;

namespace esWMS.Application.Functions.Zones.Commands
{
    internal abstract class CommonZoneValidation<T> : AbstractValidator<T>
        where T : CommonZoneCommand
    {
        protected CommonZoneValidation()
        {
            
        }
    }
}
