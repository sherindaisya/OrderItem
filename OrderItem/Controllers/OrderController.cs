using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderItem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            List<Cart> menuitem = new List<Cart>();
            string apiurl = "http://localhost:14509/api/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/menuitem/" + id);

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    string json = await response.Content.ReadAsStringAsync();
                    menuitem = JsonConvert.DeserializeObject<List<Cart>>(json);
                    Cart cart = menuitem.SingleOrDefault(item => item.Id == id);
                    cart.MenuItemId = 1;
                    cart.UserId = 1;
                    return Ok(cart);

                }
                else
                {
                    return BadRequest();
                }

            };

        }
    }
}




