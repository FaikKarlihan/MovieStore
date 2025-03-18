using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Tests.Application.DirectorOperation.Query
{
    public class GetDirectorDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public void WhenValidIdIsGiven_Director_ShouldBeReturn()
        {
            // Given
            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
            query.DirectorId = 1;
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            var director = _context.Directors.SingleOrDefault(x=> x.Id == 1);
            
            result.Should().NotBeNull();
            result.Name.Should().Be(director.Name);
            result.Surname.Should().Be(director.Surname);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            int invalidId = _context.Directors.Count()+99;

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context, _mapper);
            query.DirectorId = invalidId;
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Director not found");;
        }
    }
}