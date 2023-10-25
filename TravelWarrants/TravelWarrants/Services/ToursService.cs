using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Tours;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class ToursService : IToursService
    {
        private readonly TravelWarrantsContext _context;
        public ToursService(TravelWarrantsContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<TourDTOGet>> CreateTour(TourDTOSave tourDTO)
        {
            var companyExists = await _context.Company.AnyAsync();
            if (!companyExists)
            {
                return new ResponseDTO<TourDTOGet>() { IsSucced = false, ErrorMessage = "Add a company first" };
            }
            var tour = new Tour
            {

                Departure = tourDTO.Departure,
                Destination = tourDTO.Destination,
                IntermediateDestinations = tourDTO.IntermediateDestinations,
                Mileage = tourDTO.Mileage,
                NumberOfPassengers = tourDTO.NumberOfPassengers,
                Price = tourDTO.Price,
                Toll = tourDTO.Toll,
                Fuel = tourDTO.Fuel,
                FuelPrice = tourDTO.FuelPrice,
                StartMileage = tourDTO.StartMileage,
                EndMileage = tourDTO.EndMileage,
                Note = tourDTO.Note,
                NumberOfDays = tourDTO.NumberOfDays,
                DriverId = tourDTO.DriverId,
                VehicleId = tourDTO.VehicleId,
                ClientId = tourDTO.ClientId,
                TimeOfTour = tourDTO.TimeOfTour,

            };
            var vehicle = await _context.Vehicles.FindAsync(tourDTO.VehicleId);

            if (vehicle == null)
            {

                return new ResponseDTO<TourDTOGet>() { IsSucced = false };
            }

            vehicle.Mileage += tourDTO.Mileage;

            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();

            var newTour = new TourDTOGet
            {
                Id = tour.Id,
                ClientId = tour.ClientId,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == tourDTO.ClientId)).Name,
                Departure = tour.Departure,
                Destination = tour.Destination,
                IntermediateDestinations = tour.IntermediateDestinations,
                Mileage = tour.Mileage,
                TimeOfTour = tour.TimeOfTour,

            };


            return new ResponseDTO<TourDTOGet>() { IsSucced = true, Message = newTour };
        }

        public async Task<ResponseDTO<bool>> DeleteTour(int id)
        {
            var tourToDelete = await _context.Tours.FirstOrDefaultAsync(x => x.Id == id);

            if (tourToDelete == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false, Message = false };
            }



            _context.Tours.Remove(tourToDelete);

            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == tourToDelete.VehicleId);

            if (vehicle == null)
            {
                return new ResponseDTO<bool>() { IsSucced = false, Message = false };
            }

            vehicle.Mileage -= tourToDelete.Mileage;


            await _context.SaveChangesAsync();


            return new ResponseDTO<bool>() { IsSucced = true, Message = true };
        }

        public async Task<ResponseDTO<TourDTOGet>> EditTour(int id, TourDTOSave tourDTO)
        {
            var tourInDb = await _context.Tours.FirstOrDefaultAsync(x => x.Id == id);

            if (tourInDb == null)
            {
                return new ResponseDTO<TourDTOGet>() { IsSucced = false };
            }


            tourInDb.Departure = tourDTO.Departure;
            tourInDb.Destination = tourDTO.Destination;
            tourInDb.Mileage = tourDTO.Mileage;
            tourInDb.NumberOfPassengers = tourDTO.NumberOfPassengers;
            tourInDb.Price = tourDTO.Price;
            tourInDb.Toll = tourDTO.Toll;
            tourInDb.Fuel = tourDTO.Fuel;
            tourInDb.TimeOfTour = tourDTO.TimeOfTour;
            tourInDb.StartMileage = tourDTO.StartMileage;
            tourInDb.EndMileage = tourDTO.EndMileage;
            tourInDb.Note = tourDTO.Note;
            tourInDb.IntermediateDestinations = tourDTO.IntermediateDestinations;
            tourInDb.DriverId = tourDTO.DriverId;
            tourInDb.VehicleId = tourDTO.VehicleId;
            tourInDb.ClientId = tourDTO.ClientId;

            _context.Tours.Update(tourInDb);
            await _context.SaveChangesAsync();

            var updatedTour = new TourDTOGet
            {
                Id = tourInDb.Id,
                ClientId = tourInDb.ClientId,
                ClientName = (await _context.Clients.FirstOrDefaultAsync(c => c.Id == tourDTO.ClientId)).Name,
                Departure = tourInDb.Departure,
                Destination = tourInDb.Destination,
                IntermediateDestinations = tourInDb.IntermediateDestinations,
                Mileage = tourInDb.Mileage,
                TimeOfTour = tourInDb.TimeOfTour,

            };


            return new ResponseDTO<TourDTOGet>() { IsSucced = true, Message = updatedTour };
        }

        public async Task<ResponseDTO<TourDeleteDTO>> GetForDelete(int id)
        {
            var deleteTour = await _context.Tours
                .Include(c => c.Client)
                .Include(v => v.Vehicle)
                .Include(d => d.Driver)
                .Where(x => x.Id == id)
                .Select(x => new TourDeleteDTO
                {
                    Date = x.TimeOfTour,
                    Client = x.Client.Name,
                    Departure = x.Departure,
                    Destination = x.Destination,
                    Registration = x.Vehicle.Registration,
                    Driver = x.Driver.Name,
                    Mileage = x.Mileage,
                }).FirstOrDefaultAsync();

            return new ResponseDTO<TourDeleteDTO>() { IsSucced = true, Message = deleteTour };
        }

        public async Task<ResponseDTO<TourDTOById>> GetTour(int id)
        {
            var tourDTO = await _context.Tours.Include(c => c.Client).Include(d => d.Driver).Include(v => v.Vehicle).Where(x => x.Id == id).Select(x => new TourDTOById
            {
                Departure = x.Departure,
                Destination = x.Destination,
                IntermediateDestinations = x.IntermediateDestinations,
                Mileage = x.Mileage,
                NumberOfPassengers = x.NumberOfPassengers,
                Price = x.Price,
                Toll = x.Toll,
                Fuel = x.Fuel,
                FuelPrice = x.FuelPrice,
                StartMileage = x.StartMileage,
                EndMileage = x.EndMileage,

                Note = x.Note,
                NumberOfDays = x.NumberOfDays,
                DriverId = x.DriverId,
                VehicleId = x.VehicleId,
                ClientId = x.ClientId,
                TimeOfTour = x.TimeOfTour,
                ClientName = x.Client.Name,
                DriverName = x.Driver.Name,
                VehicleRegistration = x.Vehicle.Registration,

            }).FirstOrDefaultAsync();

            return new ResponseDTO<TourDTOById>() { IsSucced = true, Message = tourDTO };
        }

        public async Task<ResponseDTO<IEnumerable<TourDTOGet>>> GetTours()
        {
            var tours = await _context.Tours.Include(c => c.Client).ToListAsync();

            var toursDTO = tours.Select(x => new TourDTOGet
            {
                Id = x.Id,
                ClientId = x.ClientId,
                Departure = x.Departure,
                Destination = x.Destination,
                Mileage = x.Mileage,
                TimeOfTour = x.TimeOfTour,
                IntermediateDestinations = x.IntermediateDestinations,
                ClientName = x.Client.Name

            });

            return new ResponseDTO<IEnumerable<TourDTOGet>>() { IsSucced = true, Message = toursDTO };
        }
    }
}
