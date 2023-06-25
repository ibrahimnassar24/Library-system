using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

/*
namespace LibrarySystem.Models
{
    public class CartItem : Operation
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Edition { get; set; }
        public float Price_of_Selling { get; set; }
        public float Price_Of_Reservation { get; set; }
        public float Purchase_cost { get; set; }
        public float Borrow_cost { get; set; }

        public List<CartItem> Retrieve()
        {
            string sql = string.Format
                (
                "SELECT title, publisher, edition, price_of_selling, price_of_reservation, " +
                "operations.type as type, date, purchase_books.copies as copies " +
                "FROM operations " +
                "JOIN purchase " +
                "ON operations.username = '{0}' " +
                "AND operations.status = 'active' " +
                "AND operations.id = purchase.operationid " +
                "JOIN book " +
                "ON purchase.book_id = book.id ",
                UserName
                );
            
            var res = DB.Connection.Query<CartItem> ( sql ).ToList();

            
            List<CartItem> all = new List<CartItem>();
            
            foreach(var item in res)
            {
                all.Add( item );
            }
            
            sql = string.Format
                (
                "SELECT title, publisher, edition, price_of_selling, price_of_reservation, " +
                "operations.type as type, date " +
                "FROM operations " +
                "JOIN borrow_books " +
                "ON operations.username = '{0}' " +
                "AND operations.status = 'active' " +
                "AND operations.id = borrow_books.operation_id " +
                "JOIN book_store " +
                "ON borrow_books.book_id = book_store.id ",
                UserName
                );

            var res2 = DB.Connection.Query<CartItem>(sql).ToList();

            foreach(var item in res2)
            {
                all.Add ( item );
            }
            
            return all;
        }

        public void Complete()
        {
            var sql = string.Format
                (
                "UPDATE operations " +
                "SET status = 'closed', pay_method = '{1}', " +
                "date = '{2}', " +
                "total_cost = {3} " +
                "WHERE username = '{0}' " +
                "AND status = 'active' " +
                "AND type = 'purchase' ",
                UserName, Pay_Method, DateTime.Now.ToString(), Purchase_cost
                );

            var res = DB.Connection.Query(sql);

             sql = string.Format
                (
                "UPDATE operations " +
                "SET pay_method = '{1}', " +
                "status = 'ongoing', " +
                "total_cost = {2} " +
                "WHERE username = '{0}' " +
                "AND status = 'active' " +
                "AND type = 'borrow' ",
                UserName, Pay_Method, Borrow_cost
                );
            _ = DB.Connection.Query(sql);

        }
    }
}
*/