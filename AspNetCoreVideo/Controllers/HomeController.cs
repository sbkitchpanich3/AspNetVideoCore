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

        // During HTTP POST, a POST version of the action method is called with the form values
        // The names of the form controls are matched against the model properties or parameters available in the action's parameter list
        // POST action then uses that data to create, update, or delete data in the data source.
        // By default, MVC matches all properties in the form with properties in the model which is risky because you don't always want all data the form sends
        // Having a separate ViewModel bypasses this.

        [HttpPost]
        public IActionResult Create(VideoEditViewModel model) // using VideoEditViewModel as the model to base the fields for the form on
        {
            if (ModelState.IsValid)
            { 
                var video = new Video // Here we match the form inputs to the respective properties in the ViewModel
                {
                    Title = model.Title,
                    Genre = model.Genre
                };
                _videos.Add(video);
                _videos.Commit();
                return RedirectToAction("Details", new { id = video.Id });
            }

            return View();
        }

        [HttpGet] // The HTTP GET takes place when the video has not yet been edited/before pressing edit.
        public IActionResult Edit(int id)
        {
            var video = _videos.Get(id); // Get the id of the video that is being edited.
            if (video == null) // if the video doesn't exist, return to index.
                return RedirectToAction("Index");
            return View(video); // Show the current details of the video.
        }

        [HttpPost] // The HTTP POST takes place after the video has been edited/after pressing edit.
        public IActionResult Edit(int id, VideoEditViewModel model)
        {
            var video = _videos.Get(id); // Get the id of the video that is being edited.
            if (video == null || !ModelState.IsValid) // If the video doesn't exist or is invalid, simply show the old details of the video.
                return View(model);
            video.Title = model.Title; // Assign the input title from the ViewModel to the video.
            video.Genre = model.Genre; // Assign the input genre from the ViewModel to the video.
            _videos.Commit();
            return RedirectToAction("Details", new { id = video.Id });
        }

    }
}

// Since the video entity is going to contain purely data and the ViewModel is the class that is going to be DISPLAYING the data, 
// you must make ViewModel objects instead of Video objects when passing them to the view.
