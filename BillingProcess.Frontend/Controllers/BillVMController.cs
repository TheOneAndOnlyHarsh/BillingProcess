using BillingProcess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BillingProcess.Frontend.Controllers
{
    public class BillVMController : Controller
    {
        private readonly HttpClient _httpClient;

        public BillVMController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7150/api/Bill");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var bills = await response.Content.ReadFromJsonAsync<Bill[]>();
                return View(bills);
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
        public async Task<IActionResult> Create(Bill bill)
        {
            var response = await _httpClient.PostAsJsonAsync("", bill);
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
            var response = await _httpClient.GetAsync($"Bill/{id}");
            if (response.IsSuccessStatusCode)
            {
                var bill = await response.Content.ReadFromJsonAsync<Bill>();
                return View(bill);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Bill updatedBill)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(updatedBill);
            }

            // Check if the student exists
            var studentExists = await _httpClient.GetAsync($"https://localhost:7150/api/Student/{updatedBill.StudentID}");
            if (!studentExists.IsSuccessStatusCode)
            {
                ModelState.AddModelError("StudentID", "Invalid Student ID");
                return View(updatedBill);
            }

            // Make the PUT request
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7150/api/Bill/{id}", updatedBill);
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
            var response = await _httpClient.DeleteAsync($"Bill/{id}");
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
