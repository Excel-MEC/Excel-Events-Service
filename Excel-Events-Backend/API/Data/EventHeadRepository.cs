using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.EventHeads;
using API.Extensions.CustomExceptions;
using API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Data
{
    public class EventHeadRepository : IEventHeadRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public EventHeadRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<List<EventHeadForViewDto>> ListEventHeads()
        {
            var eventHeads = await _context.EventHeads.ToListAsync();
            return eventHeads.Select(e => _mapper.Map<EventHeadForViewDto>(e)).ToList();
        }

        public async Task<EventHeadForViewDto> GetEventHead(int id)
        {
            var eventHead = await _context.EventHeads.FindAsync(id);
            if (eventHead == null) throw new DataInvalidException("Invalid ID. Please re-check the ID");
            return _mapper.Map<EventHeadForViewDto>(eventHead);
        }

        public async Task<EventHead> AddEventHead(DataForAddingEventHead newEventHead)
        {
            if(newEventHead.Name == null || newEventHead.Email == null || newEventHead.PhoneNumber == null)
                throw new DataInvalidException("Incorrect input. Please re-check your Name, Email and PhoneNumber");
            var eventHeadsFromDb = await _context.EventHeads.Where(e => e.Email == newEventHead.Email).ToListAsync();
            if(eventHeadsFromDb.Count > 0) throw new DataInvalidException("This email is already associated with an EventHead");
            var newHead = new EventHead
            {
                Name = newEventHead.Name,
                Email = newEventHead.Email,
                PhoneNumber = newEventHead.PhoneNumber
            };
            await _context.EventHeads.AddAsync(newHead);
            if(await _context.SaveChangesAsync() > 0) return  newHead;
            throw new Exception("Problem in saving changes");
        }

        public async Task<EventHead> UpdateEventHead(DataForUpdatingEventHeadDto newEventHead)
        {
            var eventHeadsFromDb = await _context.EventHeads.Where(e => e.Email == newEventHead.Email || e.Id == newEventHead.Id).ToListAsync();
            if(eventHeadsFromDb.Count > 1) throw new DataInvalidException("This email is already associated with an EventHead");
            if (eventHeadsFromDb.Count == 0 || eventHeadsFromDb[0].Id != newEventHead.Id) throw new DataInvalidException("Invalid id. Please re-check the ID");
            eventHeadsFromDb[0].Name = newEventHead.Name ?? eventHeadsFromDb[0].Name;
            eventHeadsFromDb[0].Email = newEventHead.Email ?? eventHeadsFromDb[0].Email;
            eventHeadsFromDb[0].PhoneNumber = newEventHead.PhoneNumber ?? eventHeadsFromDb[0].PhoneNumber;
            if(await _context.SaveChangesAsync() > 0) return  newEventHead;
            throw new OperationInvalidException("No changes were made to update. Please re-check the input");
        }

        public async Task<EventHead> DeleteEventHead(DataForDeletingEventHeadDto dataForDeletingEventHead)
        {
            if(dataForDeletingEventHead.Id == 0 || dataForDeletingEventHead.Name == null)
                throw new DataInvalidException("Incorrect input. Please re-check the ID and Name");
            var eventHeadFromDb = await _context.EventHeads.FindAsync(dataForDeletingEventHead.Id);
            if (eventHeadFromDb == null) throw new DataInvalidException("Invalid id. Please re-check the ID");
            if( dataForDeletingEventHead.Name != eventHeadFromDb.Name) 
                throw new DataInvalidException(" Name and Id does not match. Please re-check the ID and Name");
            _context.EventHeads.Remove(eventHeadFromDb);
            if(await _context.SaveChangesAsync() > 0) return  eventHeadFromDb;
            throw new Exception("Problem in saving changes.");
        }
    }
}