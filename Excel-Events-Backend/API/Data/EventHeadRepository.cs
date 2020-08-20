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

        public async Task<bool> AddEventHead(DataForAddingEventHead newEventHead)
        {
            if(newEventHead.Name == null || newEventHead.Email == null || newEventHead.PhoneNumber == null)
                throw new DataInvalidException("Incorrect input. Please re-check your Name, Email and PhoneNumber");
            var newHead = new EventHead
            {
                Name = newEventHead.Name,
                Email = newEventHead.Email,
                PhoneNumber = newEventHead.PhoneNumber
            };
            await _context.EventHeads.AddAsync(newHead);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEventHead(DataForUpdatingEventHeadDto newEventHead)
        {
            var eventHeadFromDb = await _context.EventHeads.FindAsync(newEventHead.Id);
            if (eventHeadFromDb == null) throw new DataInvalidException("Invalid id. Please re-check the ID");
            eventHeadFromDb.Name = newEventHead.Name ?? eventHeadFromDb.Name;
            eventHeadFromDb.Email = newEventHead.Email ?? eventHeadFromDb.Email;
            eventHeadFromDb.PhoneNumber = newEventHead.PhoneNumber ?? eventHeadFromDb.PhoneNumber;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEventHead(DataForDeletingEventHeadDto dataForDeletingEventHead)
        {
            Console.WriteLine(dataForDeletingEventHead.Id);
            if(dataForDeletingEventHead.Id == 0 || dataForDeletingEventHead.Name == null)
                throw new DataInvalidException("Incorrect input. Please re-check the ID and Name");
            var eventHeadFromDb = await _context.EventHeads.FindAsync(dataForDeletingEventHead.Id);
            if (eventHeadFromDb == null) throw new DataInvalidException("Invalid id. Please re-check the ID");
            if( dataForDeletingEventHead.Name != eventHeadFromDb.Name) 
                throw new DataInvalidException(" Name and Id does not match. Please re-check the ID and Name");
            _context.EventHeads.Remove(eventHeadFromDb);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}