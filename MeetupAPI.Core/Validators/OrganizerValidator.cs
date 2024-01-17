using FluentValidation;
using MeetupAPI.Core.DTOs.DefaultDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Validators
{
    public class OrganizerValidator : AbstractValidator<DefaultOrganizerDto>
    {
        public OrganizerValidator()
        {
            RuleFor(org => org.Name).NotEmpty().MaximumLength(30);
        }
    }
}
