﻿using MeetupAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Repositories.Abstractions
{
    public interface IMeetupRepository : IBaseRepository<Meetup>
    {
    }
}
