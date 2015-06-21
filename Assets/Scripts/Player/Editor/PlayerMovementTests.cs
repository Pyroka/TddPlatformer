using UnityEngine;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Player
{
    [TestFixture]
    [Category("Player")]
    internal class PlayerMovementTests
    {
        [Test]
        public void ShouldFallIfNotOnGround()
        {
            var movement = new PlayerMovement {IsOnGround = false};

            movement.Update();

            movement.CurrentVelocity.y.Should().BeLessThan(0.0f);
        }

        [Test]
        public void ShouldNotFallIfOnGround()
        {
            var movement = new PlayerMovement { IsOnGround = true };

            movement.Update();

            movement.CurrentVelocity.y.Should().Be(0.0f);
        }

        [Test]
        public void ShouldAccelerateWhilstFalling()
        {
            var movement = new PlayerMovement { IsOnGround = false };

            movement.Update();
            var firstResult = movement.CurrentVelocity;
            movement.Update();

            movement.CurrentVelocity.y.Should().BeLessThan(firstResult.y);
        }

        [Test]
        public void ShouldMoveLeftWhenHorizontalInputIsPositive()
        {
            var movement = new PlayerMovement {HorizontalInput = 1.0f};

            movement.Update();

            movement.CurrentVelocity.y.Should().BeApproximately(movement.MaxHorizontalSpeed, 0.1f);
        }
    }
}