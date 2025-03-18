using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Applications.ActorOperations.Queries.GetActorDetail;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.Application.ActorOperations.Query
{
    public class GetActorDetailQueryTests: IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenValidIdIsGiven_Actor_ShouldBeReturn()
        {
            // Given
            var actor = new Actor
            {
                Name = "Tame",
                Surname = "Impala"
            };
            _context.Actors.Add(actor);
            _context.SaveChanges();

            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = actor.Id;
            // When
            var result = FluentActions.Invoking(()=> query.Handle()).Invoke();
            // Then
            result.Should().NotBeNull();
            result.Name.Should().Be(actor.Name);
            result.Surname.Should().Be(actor.Surname);
        }

        [Fact]
        public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // Given
            var invalidId = _context.Actors.Count()+99;

            GetActorDetailQuery query = new GetActorDetailQuery(_context, _mapper);
            query.ActorId = invalidId;
            // When && Then
            var result = FluentActions.Invoking(()=> query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Actor not found");
        }
    }
}