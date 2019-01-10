using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVideo.Entities;

namespace AspNetCoreVideo.Services
{
    public class MockVideoData : IVideoData // Going to be used to grab video data from database
    {
        private List<Video> _videos; // Setting a variable to store the video list.

        public MockVideoData()
        {
            _videos = new List<Video>
            {
                new Video { Id = 1, Genre = Models.Genres.Comedy, Title = "Shrek" },
                new Video { Id = 2, Genre = Models.Genres.Comedy, Title = "Despicable Me" },
                new Video { Id = 3, Genre = Models.Genres.Comedy, Title = "Megamind" }
            };
        }

        public IEnumerable<Video> GetAll()
        {
            return _videos;
        }

        public Video Get(int id)
        {
            return _videos.FirstOrDefault(v => v.Id.Equals(id)); // Using LINQ again.  v is the argument which represent one video object
        }

        public void Add(Video newVideo)
        {
            newVideo.Id = _videos.Max(v => v.Id) + 1; // Since new videos do not yet have an id, we have to make one somehow.  Find max id out of existing videos, add one to that, thats the new id.
            _videos.Add(newVideo);
        }

        public int Commit()
        {
            return 0;
        }
    }
}
