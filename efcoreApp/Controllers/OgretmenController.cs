using System.IO.Compression;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers;

public class OgretmenController : Controller
{
    private readonly DataContext _context;
    public OgretmenController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(await _context.Ogretmenler.ToListAsync());
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Ogretmen model)
    {
        if (ModelState.IsValid)
        {
            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var ogretmen = await _context.Ogretmenler
                                    .Include(x => x.Kurslar)
                                    .FirstOrDefaultAsync(x => x.OgretmenId == id);
        return View(ogretmen);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Ogretmen model)
    {
        if (id != model.OgretmenId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ogretmen = await _context.Ogretmenler.FindAsync(id);

        if (ogretmen == null)
        {
            return NotFound();
        }

        return View(ogretmen);
    }


    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var ogretmen = await _context.Ogretmenler.FindAsync(id);

        if (ogretmen == null)
        {
            return NotFound();
        }

        _context.Ogretmenler.Remove(ogretmen);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}