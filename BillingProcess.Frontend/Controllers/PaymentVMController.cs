using BillingProcess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BillingProcess.Frontend.Controllers
{
    public class PaymentVMController : Controller
    {
        private readonly HttpClient _httpClient;

        public PaymentVMController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7150/api/Payment");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                var payments = await response.Content.ReadFromJsonAsync<Payment[]>();
                return View(payments);
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
        public async Task<IActionResult> Create(Payment payment)
        {
            var response = await _httpClient.PostAsJsonAsync("", payment);
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
            var response = await _httpClient.GetAsync($"Payment/{id}");
            if (response.IsSuccessStatusCode)
            {
                var payment = await response.Content.ReadFromJsonAsync<Payment>();
                return View(payment);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(payment);
            }

            // Check if the student exists
            var billExists = await _httpClient.GetAsync($"https://localhost:7150/api/Bill/{payment.BillId}");
            if (!billExists.IsSuccessStatusCode)
            {
                ModelState.AddModelError("Bill", "Invalid Bill ID");
                return View(payment);
            }

            // Make the PUT request
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7150/api/Payment/{id}", payment);
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
            var response = await _httpClient.DeleteAsync($"Payment/{id}");
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
