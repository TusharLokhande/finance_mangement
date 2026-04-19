using Application.Interfaces.Persistence.Repository;
using Domain.Entity;
using InfraStructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraStructure.Repository
{
    internal class TagRepository : GenericRepository<Tags>, ITagRepository
    {
        private readonly AppDbContext _db;

        public TagRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public List<Guid> InsertTag(HashSet<string> tags)
        {
            if (tags == null || tags.Count == 0)
                return new List<Guid>();

            // Normalize (important — avoids duplicates like "AI" vs "ai")
            var normalizedTags = tags
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim().ToLower())
                .ToHashSet();

            // Fetch existing tags
            var existingTags = _db.Tags
                .Where(t => normalizedTags.Contains(t.TagName))
                .ToList();

            var existingTagNames = existingTags
                .Select(t => t.TagName)
                .ToHashSet();

            // Find new tags
            var newTags = normalizedTags
                .Where(t => !existingTagNames.Contains(t))
                .Select(t => new Tags(t))
                .ToList();

            // Insert new ones
            if (newTags.Any())
            {
                _db.Tags.AddRange(newTags);
                _db.SaveChanges();
            }

            // Combine and return IDs
            return existingTags
                .Concat(newTags)
                .Select(t => t.Id)
                .ToList();
        }
    }
}
