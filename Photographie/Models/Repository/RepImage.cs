using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photographie.Models.Repository
{
    public class RepImage : Repository<Photo_>
    {
        public RepImage(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public async Task Add(Photo_ entity)
        {
           await Context.GetPhotos.AddAsync(entity);
           await Save();
        }

        public async Task Delete(int id)
        {
            var getPhoto = await Find(id);
            Context.GetPhotos.Remove(getPhoto);
            await Save();
        }

        public async Task<Photo_>Find(int id)
        {
            var getImage= await Context.GetPhotos.SingleOrDefaultAsync(p => p.Id == id);
            return getImage;
        }

        public async Task<List<Photo_>> List()
        {
            var listImage= await Context.GetPhotos.ToListAsync();
            return listImage;
        }

        public async Task Update(int id, Photo_ entity)
        {
            var getImage = await Context.GetPhotos.SingleOrDefaultAsync(p => p.Id == id);
            Context.GetPhotos.Update(getImage);
            await Save();
        }
        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
