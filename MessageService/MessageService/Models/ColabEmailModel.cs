using MassTransit;
using MassTransit.Riders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ColabEmailModel : IConsumer<ColabEmailModel>
    {
        public long ColabId { get; set; }
        public string Email { get; set; }

        public async Task Consume(ConsumeContext<ColabEmailModel> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Email);
        }
    }
}