using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Exceptions;
using Domain.Entities;
using Infra.Interfaces;
using Services.DTOs;
using Services.Interfaces;

namespace Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<DepartmentDto> Create(DepartmentDto departmentDto)
        {
            var departmentExist = await _departmentRepository.GetByName(departmentDto.DepartmentName);

            if (departmentExist != null)
            {
                throw new DomainExceptions("Já existe um departamento cadastrado com esse nome");
            }

            var departmento = _mapper.Map<Department>(departmentDto);
            if (departmento == null)
            {
                throw new Exception("Falha no mapeamento do departamento.");
            }
            departmento.Validate();

            var departmentCreated = await _departmentRepository.Create(departmento);

            return _mapper.Map<DepartmentDto>(departmentCreated);
        }

        public async Task<DepartmentDto> Get(long id)
        {
            var department = await _departmentRepository.Get(id);

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task<List<DepartmentDto>> Get()
        {
            var department = await _departmentRepository.Get();

            return _mapper.Map<List<DepartmentDto>>(department);
        }

        public async Task<DepartmentDto> GetDepartmentById(long departmentId)
        {
            var department = await _departmentRepository.Get(departmentId);

            if (department == null)
            {
                throw new DomainExceptions("Nenhum departamento com esse Id foi encontrado");
            }

            return _mapper.Map<DepartmentDto>(department);
        }

        public async Task Remove(long id)
        {
            await _departmentRepository.Remove(id);
        }

        public async Task<DepartmentDto> Update(DepartmentDto departmentDto)
        {
            var departmentExist = await _departmentRepository.Get(departmentDto.Id);

            if (departmentExist == null)
                throw new DomainExceptions("Nao existe nenhum usuario com o Id informado");

            var department = _mapper.Map<Department>(departmentDto);

            var departmentUpdated = await _departmentRepository.Update(department);

            return _mapper.Map<DepartmentDto>(departmentUpdated);
        }
    }
}