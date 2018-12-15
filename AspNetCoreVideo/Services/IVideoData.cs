using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreVideo.Entities;

namespace AspNetCoreVideo.Services
{
    public interface IVideoData
    {
        IEnumerable<Video> GetAll();
        Video Get(int id); // Makes it possible to retrieve a video by ID.
        void Add(Video newVideo);
    }
}
