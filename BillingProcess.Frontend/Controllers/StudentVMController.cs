using BillingProcess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BillingProcess.Frontend.Controllers
{
    public class StudentVMController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentVMController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7150/api/Student");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var students = await response.Content.ReadFromJsonAsync<Student[]>();
                return View(students);
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"Student/{id}");
            if (response.IsSuccessStatusCode)
            {
                var student = await response.Content.ReadFromJsonAsync<Student>();
                return View(student);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            // Make the PUT request
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7150/api/Student/{id}", student);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"Student/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
