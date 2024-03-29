﻿using PETSHOP.Common;
using PETSHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PETSHOP.Utils
{
    public class GetApiDeliveryProducts
    {
        public static IEnumerable<DeliveryProduct> GetDeliveryProducts()
        {
            IEnumerable<DeliveryProduct> res = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.BASE_URI);
                var responseTask = client.GetAsync(Constants.DELIVERY_PRODUCT);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DeliveryProduct>>();
                    readTask.Wait();

                    res = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    res = Enumerable.Empty<DeliveryProduct>();
                }
            }

            return res;
        }

        public static void Update(DeliveryProduct delivery, string token)
        {
            using (var client = Common.HelperClient.GetClient(token))
            {
                client.BaseAddress = new Uri(Common.Constants.BASE_URI);

                var putTask = client.PutAsJsonAsync<DeliveryProduct>(Constants.DELIVERY_PRODUCT + "/" + delivery.DeliveryProductId, delivery);
                putTask.Wait();
            }
        }
    }
}
