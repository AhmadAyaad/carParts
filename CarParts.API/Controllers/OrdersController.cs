using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarParts.API.Controllers.Dtos;
using CarParts.API.Dtos;
using CarParts.Infrastructure.Repository;
using CarParts.Infrastructure.UnitOfWork;
using CarParts.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace CarParts.API.Controllers
{
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private const string Message = "these products are out of stock";
        readonly IUnitOfWork _unitOfWork;
        readonly IOrderRepository _orderRepository;
        public OrdersController(IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
        }


        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderForCreateDto orderForCreateDto, int userId)
        {

            var currentLoggedInUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await _unitOfWork.UserRepository.GetUser(currentLoggedInUser);

            if (currentLoggedInUser != userId)
                return Unauthorized();

            var notAvaibleProducts = await _unitOfWork.ProductRepository.
                                GetProductQuantityLessThanZero(orderForCreateDto.Products);

            if (notAvaibleProducts != null)
            {
                var notAvaiableProductNames = _unitOfWork.ProductRepository
                                    .GetNotAviableProductNames(ref notAvaibleProducts);
                return StatusCode(424, new { Message, notAvaiableProductNames, notAvaibleProducts });
            }
            var order = Map(orderForCreateDto);
            try
            {
                order.Products = orderForCreateDto.Products;
                await _unitOfWork.GenericOrderRepository.Create(order);
                userFromRepo.Orders.Add(order);
                await _unitOfWork.UserRepository.UpdateUserInfo(userFromRepo, currentLoggedInUser);

                await _unitOfWork.SaveChangesAysnc();
                return StatusCode(201);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListAllOrders(int userId)
        {
            var orders = await _orderRepository.GetUserOrders(userId);
            var ordersToReturn = orders.Select(order => new OrderForListDto()
            {
                CartId = order.CartId,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                Item = order.Item,
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus,
                Products = order.Products,
                UserId = order.UserId

            });

            if (ordersToReturn != null)
                return Ok(ordersToReturn);
            return NotFound();
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetUserOrder(int orderId, int userId)
        {
            var currentLoggedInUser = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentLoggedInUser != userId)
                return Unauthorized();
            var user = await _unitOfWork.UserRepository.GetUser(currentLoggedInUser);
            var order = user.Orders.SingleOrDefault(o => o.OrderId == orderId);

            var orderToReturn = new OrderForListDto
            {
                CartId = order.CartId,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                OrderId = order.OrderId,
                Item = order.Item,
                OrderStatus = order.OrderStatus,
                Products = order.Products,
                UserId = order.UserId
            };
            if (orderToReturn != null)
                return Ok(orderToReturn);
            return NotFound();
        }


        private Order Map(OrderForCreateDto orderForCreateDto)
        {
            var order = new Order
            {
                CartId = orderForCreateDto.CartId,
                CreatedAt = orderForCreateDto.CreatedAt,
                Item = orderForCreateDto.Item,
                OrderStatus = orderForCreateDto.OrderStatus,
                Products = orderForCreateDto.Products,
                UserId = orderForCreateDto.UserId
            };
            return order;
        }
    }
}
