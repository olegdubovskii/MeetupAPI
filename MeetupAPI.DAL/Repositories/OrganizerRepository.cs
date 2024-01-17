using MeetupAPI.DAL;
using MeetupAPI.DAL.Entities;
using MeetupAPI.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.DAL.Repositories
{
    public class OrganizerRepository : BaseRepository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(MeetupDatabaseContext dbContext) : base(dbContext) { }
    }
}
