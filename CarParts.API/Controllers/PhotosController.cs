using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParts.API.Helper;
using CarParts.Infrastructure.Repository;
using CarParts.Model;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Reflection;
using CloudinaryDotNet.Actions;
using CarParts.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using CarParts.API.Controllers.Dtos;

namespace CarParts.API.Controllers
{
    [Route("api/products/{productId}/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        readonly IRepository<Product> _genericRepository;
        readonly IProductRepository _productRepository;
        readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        readonly Cloudinary _cloudinary;

        public PhotosController(IRepository<Product> genericRepository,
                                IProductRepository productRepository,
                                IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _genericRepository = genericRepository;
            _productRepository = productRepository;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account
            {
                Cloud = cloudinaryConfig.Value.CloudName,
                ApiKey = cloudinaryConfig.Value.ApiKey,
                ApiSecret = cloudinaryConfig.Value.ApiSecret
            };
            _cloudinary = new Cloudinary(account);
        }

        //api/products/{productId}/[controller]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id, [FromRoute] int productId)
        {
            var photo = await _productRepository.GetProductPhoto(id, productId);
            if (photo != null)
                return Ok(photo);
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddProductPhoto([FromRoute] int productId,
                                            [FromForm] PhotoForCreationDto photoForCreationDto)
        {

            var product = await _genericRepository.GetById(productId);
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500)
                        .Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = new Photo
            {
                DateAdded = photoForCreationDto.DateAdded,
                PublicId = photoForCreationDto.PublicId,
                Url = photoForCreationDto.Url
            };

            if (!product.Photos.Any(photo => photo.IsMain==true))
                photo.IsMain = true;
            product.Photos.Add(photo);
            if (await _productRepository.SaveChangesAsync())
                return StatusCode(201);
            return BadRequest("cannot create photo");

        }
        //api/products/{productId}/[controller]

        [HttpPatch("{photoId}")]
        public async Task<IActionResult> SetProductMainPhoto(int photoId, [FromRoute] int productId)
        {
            //var photo = product.Photos.SingleOrDefault(p => p.IsMain == true);
            //var product = await _genericRepository.GetById(productId);

            var photo = await _productRepository.GetProductPhoto(photoId, productId);
            if (photo != null && photo.IsMain == false)
            {
                photo.IsMain = true;
                await _productRepository.SaveChangesAsync();
                return Ok(photo);
            }
            else if (photo != null)
            {
                photo.IsMain = true;
                await _productRepository.SaveChangesAsync();
                return Ok(photo);
            }
            return BadRequest("Cannot set main photo");
        }

    }
}
