using System;
using System.Collections.Generic;
using System.Linq;

using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
	private readonly ILogger<ProductController> _logger;
	private readonly IDataAccess<Product> _dataAccess;

	public ProductController(ILogger<ProductController> logger, IDataAccess<Product> dataAccess)
	{
		_logger = logger;
		_dataAccess = dataAccess;
	}

	[HttpGet]
	[Route("/")]
	public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5)
	{
		return _dataAccess.List(pageStart, pageSize);
	}

	[HttpGet]
	[Route("/euro/")]
	public IEnumerable<ProductEuro> GetInEuro(int pageStart = 0, int pageSize = 5)
	{
		var result = _dataAccess.List(pageStart, pageSize);
		return result.Select(p => new ProductEuro { Name = p.Name, PriceInEuro = p.PriceInPounds * (decimal)1.11 });
	}
}