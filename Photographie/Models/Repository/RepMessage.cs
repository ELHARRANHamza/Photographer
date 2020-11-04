using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.Models.Repository
{
    public class RepMessage : Repository<Message_>
    {
        public ApplicationDbContext Context { get; }

        public RepMessage(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task Add(Message_ entity)
        {
            await Context.messages.AddAsync(entity);
            await Save();
        }

        public async Task Delete(int id)
        {
            var find_Message = await Find(id);
            Context.messages.Remove(find_Message);
            await Save();
        }

        public async Task<Message_> Find(int id)
        {
            var find_Message = await Context.messages.SingleOrDefaultAsync(m => m.Id == id);
            return find_Message;
        }

        public async Task<List<Message_>> List()
        {
            var listeMessage = await Context.messages.OrderByDescending(m =>m.dateEnvoyer).ToListAsync();
            return listeMessage;
        }

        public Task Update(int id, Message_ entity)
        {
            throw new NotImplementedException();
        }
        public async Task Save()
        {
           await Context.SaveChangesAsync();
        }
    }
}
