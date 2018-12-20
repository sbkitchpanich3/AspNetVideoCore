using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVideo.Data;
using AspNetCoreVideo.Entities;

namespace AspNetCoreVideo.Services
{
    public class SqlVideoData : IVideoData
    {
        private VideoDbContext _db; // Will hold the context needed to communicate with the database

        public SqlVideoData(VideoDbContext db)
        {
            _db = db;
        }

        public void Add(Video newVideo)
        {
            _db.Add(newVideo);
            _db.SaveChanges();
        }

        public Video Get(int id)
        {
            return _db.Find<Video>(id);
        }

        public IEnumerable<Video> GetAll()
        {
            return _db.Videos;
        }
    }
}
