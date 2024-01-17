using FluentValidation;
using MeetupAPI.Core.DTOs.DefaultDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Core.Validators
{
    public class MeetupValidator : AbstractValidator<DefaultMeetupDto>
    {
        public MeetupValidator() 
        {
            RuleFor(dto => dto.Title).NotEmpty().MaximumLength(50);
            RuleFor(dto => dto.OrganizerId).NotEmpty();
            RuleFor(dto => dto.Location).MaximumLength(100);
        }
    }
}
