using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MenuItemListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        public static List<MenuItem> menulist = new List<MenuItem>()
        {
            new MenuItem { Id = 1, Name = "Burger", freeDelivery = true, Price = 60.00,  dateOfLaunch = DateTime.Now, Active = true },
            new MenuItem { Id = 2, Name = "Pizza", freeDelivery = true, Price = 250.00,  dateOfLaunch = DateTime.Now ,Active = true },
            new MenuItem { Id = 3, Name = "Pasta", freeDelivery = true, Price = 160.00,  dateOfLaunch = DateTime.Now  , Active = true },
            new MenuItem { Id = 4, Name = "Garlic Bread", freeDelivery = true, Price = 99.00, dateOfLaunch = DateTime.Now  ,   Active = true },
        };

        [HttpGet]
        [Route("Gets")]
        public List<MenuItem> Get()
        {
            return menulist;
        }

        [HttpGet]
        [Route("GetName")]
        public List<string> GetName()
        {
            List<string> names = new List<string>();
            foreach (MenuItem menu in menulist)
            {
                names.Add(menu.Name);
            }
            return names;
        }

        [HttpGet]
        [Route("Get/{menuitemid}")]
        public string Get(int menuitemid)
        {
            MenuItem Menu = (from menu in menulist
                             where menu.Id == menuitemid
                             select menu).SingleOrDefault();
            return Menu.Name;
        }

    }
}