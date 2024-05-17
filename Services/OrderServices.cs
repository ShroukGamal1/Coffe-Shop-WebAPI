using Coffe_Shop_WebAPI.DTO.CartDTO;
using Coffe_Shop_WebAPI.DTO.OrderDTO;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.UnitOfWork;

namespace Coffe_Shop_WebAPI.Services
{
    public class OrderServices
    {
        unitOfWork<Order> UnitOfWork;
        CartServices cartServices;
        public OrderServices(unitOfWork<Order> UnitOfWork,CartServices cartServices)
        {
            this.UnitOfWork = UnitOfWork;
            this.cartServices= cartServices;
        }
        public List<OrderDTO> GetAll()
        {
            List<Order> Orders = UnitOfWork.Entity.GetAll();
            List<OrderDTO> OrdersDTO = new List<OrderDTO>();
            if (Orders == null)
            {
                return null;
            }
            else
            {
                foreach (var order in Orders)
                {
                    OrderDTO orderDTO = new OrderDTO (order.Id, order.State, order.CheckOutDate, order.TotalPrice,order.UserId);
                    OrdersDTO.Add(orderDTO);
                }
                return OrdersDTO;
            }
        }

        public OrderDTO Get(int id)
        {
            Order order = UnitOfWork.Entity.GetById(id);
            if (order == null)
            {
                return null;
            }
            else
            {
                OrderDTO orderDTO = new OrderDTO(order.Id, order.State, order.CheckOutDate, order.TotalPrice, order.UserId);

                return orderDTO;
            }
        }
        public void Delete(int id)
        {
            Order order = UnitOfWork.Entity.GetById(id);

            UnitOfWork.Entity.Delete(order);
        }
        public OrderDTO Add(AddOrderDTO orderDTO)
        {

            Order order = new Order
            {
                State = orderDTO.State,
                CheckOutDate = orderDTO.CheckOutDate,
                TotalPrice = orderDTO.TotalPrice,
                UserId = orderDTO.UserId
            };

            order.State = 'C';

            UnitOfWork.Entity.Add(order);

            UnitOfWork.savechanges();

            OrderDTO orderO = new OrderDTO(order.Id, order.State, order.CheckOutDate, order.TotalPrice, order.UserId);

            return orderO;
        }

        public void Update(OrderDTO orderDTO)
        {
            if (orderDTO != null)
            {
                Order order = new Order()
                {
                    Id = orderDTO.Id,
                    State = orderDTO.State,
                    CheckOutDate = orderDTO.CheckOutDate,
                    TotalPrice = orderDTO.TotalPrice,
                    UserId = orderDTO.UserId
                };
                UnitOfWork.Entity.Update(order);
            }
        }
        public void Save()
        {
            UnitOfWork.Entity.Save();
        }
        public CartOrderDTO GetOrderWithStateC(string userId)
        {
            Order cart= UnitOfWork.Entity.getElement(o => o.State == 'C' && o.UserId == userId,null);
            if (cart == null)
            {
                return null;
            }
            List<cartDTO> products = cartServices.Get(cart.Id);
            
            CartOrderDTO cartDTO = new CartOrderDTO(cart.Id, cart.State, cart.CheckOutDate, cart.TotalPrice, cart.UserId,products);
            return cartDTO;
        }

    }
}
