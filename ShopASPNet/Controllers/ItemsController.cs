using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopASPNet.Interfaces;
using ShopASPNet.Models.Items;
using ShopASPNet.Models.Requests;

namespace ShopASPNet.Controllers;

public class ItemsController : Controller {
    private readonly IItemRepository _itemRepository;
    
    public ItemsController(IItemRepository itemRepository) {
        _itemRepository = itemRepository;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> Index() {
        var items = await _itemRepository.GetAllAsync();
        return View(items);
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Add() {
        return View();
    }
    
    [HttpPost]
    [Route("[controller]/Add")]
    [Authorize]
    public async Task<IActionResult> Add(ItemRequest itemRequest) {
        var creationDate = DateTime.Now;
        
        var item = new Item {
            Name = itemRequest.Name,
            Description = itemRequest.Description,
            Price = itemRequest.Price,
            Created = creationDate,
            LastEdited = creationDate,
        };
        
        var result = await _itemRepository.AddAsync(item);
        
        if (result is false)
            return Forbid();
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(int id) {
        var item = await _itemRepository.GetByIdAsync(id);
        return View(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(Item item) {
        item.LastEdited = DateTime.Now;
        await _itemRepository.UpdateAsync(item);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Delete(int id) {
        await _itemRepository.RemoveByIdAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id) {
        var item = await _itemRepository.GetByIdAsync(id);
        return View(item);
    }
}