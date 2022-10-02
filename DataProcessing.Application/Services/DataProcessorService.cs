using AutoMapper;
using DataProcessing.Application.Contracts;
using DataProcessing.Application.Exceptions;
using DataProcessing.Application.Models;
using DataProcessing.Domain.Entities;

namespace DataProcessing.Application.Services
{
    internal class DataProcessorService : IDataProcessorService
    {
        private readonly IMapper _mapper;
        private readonly IDataJobRepository _dataJobRepository;
        private readonly ITaskProcessor _taskProcessor;

        public DataProcessorService(
            IMapper mapper,
            IDataJobRepository dataJobRepository,
            ITaskProcessor taskProcessor)
        {
            _mapper = mapper;
            _dataJobRepository = dataJobRepository;
            _taskProcessor = taskProcessor;
        }

        public IEnumerable<DataJobDto> GetAllDataJobs()
        {
            var allJobs = _dataJobRepository.GetAll();

            return _mapper.Map<IEnumerable<DataJobDto>>(allJobs);
        }

        public IEnumerable<DataJobDto> GetDataJobsByStatus(DataJobStatusDto statusDto)
        {
            var jobs = _dataJobRepository.GetDataJobsByStatus((DataJobStatus)statusDto);

            return _mapper.Map<IEnumerable<DataJobDto>>(jobs);
        }

        public DataJobDto GetDataJob(Guid id)
        {
            var itemToGet = _dataJobRepository.Get(id);

            if (itemToGet == null)
            {
                throw new NotFoundException(nameof(GetDataJob), id);
            }

            return _mapper.Map<DataJobDto>(itemToGet);
        }

        public DataJobDto Create(DataJobDto dataJobDto)
        {
            var id = Guid.NewGuid();
            dataJobDto.Id = id;

            var item = _mapper.Map<DataJob>(dataJobDto);
            foreach (var linkDto in dataJobDto.Links)
            {
                var link = _mapper.Map<Link>(linkDto);
                item.AddLink(link);
            }

            _dataJobRepository.AddOrUpdate(item);

            return dataJobDto;
        }

        public DataJobDto Update(DataJobDto dataJobDto)
        {
            var itemToUpdate = _dataJobRepository.Get(dataJobDto.Id);

            if (itemToUpdate == null)
            {
                throw new NotFoundException(nameof(Update), dataJobDto.Id);
            }

            var item = _mapper.Map<DataJob>(dataJobDto);
            foreach (var linkDto in dataJobDto.Links)
            {
                var link = _mapper.Map<Link>(linkDto);
                item.AddLink(link);
            }

            _dataJobRepository.AddOrUpdate(item);

            return dataJobDto;
        }

        public void Delete(Guid dataJobId)
        {
            var itemToDelete = _dataJobRepository.Get(dataJobId);

            if (itemToDelete == null)
            {
                throw new NotFoundException(nameof(Delete), dataJobId);
            }

            _dataJobRepository.Remove(dataJobId);
        }

        public bool StartBackgroundProcess(Guid dataJobId)
        {
            var itemToProcess = _dataJobRepository.Get(dataJobId);

            if (itemToProcess == null)
            {
                throw new NotFoundException(nameof(StartBackgroundProcess), dataJobId);
            }

            if (_taskProcessor.IsProcessing(itemToProcess))
            {
                return false;
            }

            _taskProcessor.Process(itemToProcess);

            return true;
        }

        public DataJobStatusDto GetBackgroundProcessStatus(Guid dataJobId)
        {
            var itemToProcess = _dataJobRepository.Get(dataJobId);

            if (itemToProcess == null)
            {
                throw new NotFoundException(nameof(GetBackgroundProcessStatus), dataJobId);
            }

            var item = _mapper.Map<DataJobDto>(itemToProcess);

            return item.Status;
        }

        public List<string> GetBackgroundProcessResults(Guid dataJobId)
        {
            var itemToProcess = _dataJobRepository.Get(dataJobId);

            if (itemToProcess == null)
            {
                throw new NotFoundException(nameof(GetBackgroundProcessResults), dataJobId);
            }

            return itemToProcess.Results.ToList();
        }
    }
}
