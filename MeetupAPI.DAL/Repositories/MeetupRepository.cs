using MeetupAPI.DAL;
using MeetupAPI.DAL.Entities;
using MeetupAPI.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Repositories
{
    public class MeetupRepository : BaseRepository<Meetup>, IMeetupRepository
    {
        public MeetupRepository(MeetupDatabaseContext dbContext) : base(dbContext) { }
    }
}
