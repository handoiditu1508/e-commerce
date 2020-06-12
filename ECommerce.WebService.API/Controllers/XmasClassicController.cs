using ECommerce.WebService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.WebService.API.Controllers
{
	//[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class XmasClassicController : ControllerBase
	{
		private static List<Song> songs = new List<Song>(new Song[] {
			new Song { Id=1, Name = "Santa Claus Is Coming on Moms" },
			new Song { Id=2, Name = "Jingle Balls" },
			new Song { Id=3, Name = "I'm Dreaming of a White Woman" },
			new Song { Id=4, Name = "Frosty the Dopeman" },
			new Song { Id=5, Name = "All I Want for Christmas Is the Charges Dropped" },
			new Song { Id=6, Name = "Deez Nuts Roasting On an Open Fire" },
			new Song { Id=7, Name = "A Sleigh Ride In My '64" },
			new Song { Id=8, Name = "Straight Outta The North Pole" },
			new Song { Id=9, Name = "Ante Up, Bitch, It's Christmas" }
		});

		public XmasClassicController() { }

		/*[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromBody]LoginModel model)
		{
			return Ok(model);
		}*/

		[HttpGet]
		public async Task<IEnumerable<Song>> Get() => songs;

		[HttpGet("{id}")]
		public async Task<Song> Get(int id) => songs.FirstOrDefault(s => s.Id == id);

		[HttpPost]
		public async Task<Song> Add([FromBody]Song song)
		{
			int id = songs.Any() ? (songs.Max(s => s.Id) + 1) : 1;
			song.Id = id;
			songs.Add(song);
			return song;
		}

		[HttpPut("{id}")]
		public async Task<Song> Update(int id, [FromBody]Song song)
		{
			if (song.Id == id)
			{
				Song oldSong = songs.FirstOrDefault(s => s.Id == id);
				songs.Remove(oldSong);
				songs.Add(song);
				return oldSong;
			}
			return null;
		}

		[HttpDelete("{id}")]
		public async Task<Song> Delete(int id)
		{
			Song oldSong = songs.FirstOrDefault(s => s.Id == id);
			songs.Remove(oldSong);
			return oldSong;
		}
	}
}
