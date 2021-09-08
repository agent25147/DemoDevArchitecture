﻿using DataAccess.Abstract;
using FluentAssertions;
using global::Business.Handlers.UserGroups.Commands;
using global::Business.Handlers.UserGroups.Queries;
using global::Core.Aspects.Autofac.Transaction;
using global::Core.Entities.Concrete;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static global::Business.Handlers.UserGroups.Commands.CreateUserGroupCommand;
using static global::Business.Handlers.UserGroups.Commands.DeleteUserGroupCommand;
using static global::Business.Handlers.UserGroups.Commands.UpdateUserGroupCommand;
using static global::Business.Handlers.UserGroups.Queries.GetUserGroupsQuery;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class UserGroupsTests
    {
        private Mock<IUserGroupRepository> _userGroupRepository;

        private GetUserGroupsQueryHandler _getUserGroupsQueryHandler;
        private CreateUserGroupCommandHandler _createUserGroupCommandHandler;
        private UpdateUserGroupCommandHandler _updateUserGroupCommandHandler;
        private DeleteUserGroupCommandHandler _deleteUserGroupCommandHandler;

        [SetUp]
        public void Setup()
        {
            _userGroupRepository = new Mock<IUserGroupRepository>();
            _getUserGroupsQueryHandler = new GetUserGroupsQueryHandler(_userGroupRepository.Object);
            _createUserGroupCommandHandler = new CreateUserGroupCommandHandler(_userGroupRepository.Object);
            _updateUserGroupCommandHandler = new UpdateUserGroupCommandHandler(_userGroupRepository.Object);
            _deleteUserGroupCommandHandler = new DeleteUserGroupCommandHandler(_userGroupRepository.Object);
        }

        [Test]
        public void Handler_GetList()
        {
            var userGroup = new UserGroup() { GroupId = 1, UserId = 1 };
            _userGroupRepository.Setup(x => x.GetListAsync(null)).
                            ReturnsAsync(new List<UserGroup>() { userGroup }.AsQueryable());

            var result = _getUserGroupsQueryHandler.Handle(new GetUserGroupsQuery(), new System.Threading.CancellationToken()).Result;
            result.Data.Should().HaveCount(1);
        }

        [Test]
        public void Handler_CreateUserGroup()
        {
            var createUserCommand = new CreateUserGroupCommand();
            createUserCommand.UserId = 1;
            createUserCommand.GroupId = 1;

            var result = _createUserGroupCommandHandler.Handle(createUserCommand, new System.Threading.CancellationToken()).Result;
            result.Success.Should().BeTrue();

        }

        [Test]
        public void Handler_UpdateUserGroup()
        {
            var updateUserCommand = new UpdateUserGroupCommand();
            updateUserCommand.GroupId = new int[] { 1 };
            updateUserCommand.UserId = 1;

            var result = _updateUserGroupCommandHandler.
                            Handle(updateUserCommand, new System.Threading.CancellationToken()).Result;

            result.Success.Should().BeTrue();

        }

        [Test]
        public void Handler_DeleteUser()
        {
            var deleteUserCommand = new DeleteUserGroupCommand();
            var result = _deleteUserGroupCommandHandler.
                            Handle(deleteUserCommand, new System.Threading.CancellationToken()).Result;

            result.Success.Should().BeTrue();
        }

        [Test]
        [TransactionScopeAspectAsync]
        public async Task Handler_TransactionScopeAspectAsyncTest()
        {
            await SomeMethodInTheCallStackAsync().ConfigureAwait(false);
        }

        private static async Task SomeMethodInTheCallStackAsync()
        {
            const int delayAmount = 500;

            await Task.Delay(delayAmount).ConfigureAwait(false);
        }
    }
}
