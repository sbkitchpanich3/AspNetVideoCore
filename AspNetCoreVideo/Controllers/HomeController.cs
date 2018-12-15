using Microsoft.AspNetCore.Mvc;
using AspNetCoreVideo.Services;
using System.Linq;
using System;
using AspNetCoreVideo.Models;
using AspNetCoreVideo.ViewModels;
using AspNetCoreVideo.Entities;

namespace AspNetCoreVideo.Controllers
{
    public class HomeController : Controller  // inherit controller class to use IEnumerable
    {
        private IVideoData _videos;  //holds the data fetched from the service.  IVideoData is the interface that was created

        public HomeController(IVideoData videos)
        {
            _videos = videos;  // makes the video service available throughout the controller via injection
        }


        public ViewResult Index()
        {
            // converts each video into a VideoViewModel object, stored into the model field which is passed to the view.
            // the "video" parameter sets the variable of the object being grabbed from the list
            var model = _videos.GetAll().Select(video =>
                new VideoViewModel
                {
                    Id = video.Id,
                    Title = video.Title,
                    Genre = video.Genre.ToString() // take genre from entity, turn it into string
                });
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _videos.Get(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(new VideoViewModel  // display ViewModel of the received video
                {
                    Id = model.Id,
                    Title = model.Title,
                    Genre = model.Genre.ToString() // take genre from the model (video entity), turn it into string
                }
            );
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VideoEditViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var video = new Video
                {
                    Title = model.Title,
                    Genre = model.Genre
                };
                _videos.Add(video);
                return RedirectToAction("Details", new { id = video.Id });
            }

            return View();
        }

    }
}

// Since the video entity is going to contain purely data and the ViewModel is the class that is going to be DISPLAYING the data, 
// you must make ViewModel objects instead of Video objects when passing them to the view.
