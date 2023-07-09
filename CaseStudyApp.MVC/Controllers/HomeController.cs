using CaseStudyApp.API.Dtos;
using CaseStudyApp.API.Entities;
using CaseStudyApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CaseStudyApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
       

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7028/");
        }

        public async Task<IActionResult> Index()
        {
            List<NoteDto> notes = await GetAllNotes();

            var data = notes.Select(x => new TreeViewNode
            {
                id = x.Id.ToString(),
                parent = x.ParentId != null ? x.ParentId.ToString() : "#",
                text = x.Title,
                data = x.Content
            });

            ViewBag.Notes = JsonConvert.SerializeObject(data);

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteDto request)
        {
            if(ModelState.IsValid)
            {
                await CreateNoteAPI(request);
                return RedirectToAction("Index");
            }
            return View(request);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await DeleteNoteAPI(id);
            return Ok();
        }

        private async Task<List<NoteDto>> GetAllNotes()
        {
            var response = await _httpClient.GetAsync("api/note");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<NoteDto>>(content);
            return notes;
        }

        private async Task CreateNoteAPI(NoteDto request)
        {
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/Note", content);
            response.EnsureSuccessStatusCode();
        }

        private async Task DeleteNoteAPI(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Note/{id}");
            response.EnsureSuccessStatusCode();
        }

    }
}