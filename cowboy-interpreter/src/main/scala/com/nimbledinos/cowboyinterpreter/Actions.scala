package com.nimbledinos.cowboyinterpreter

import cats.syntax.either._
//object Actions {
//  case class Join(userId: UUID)
//  case class ChooseClass(userId: UUID, clas: Class)
//  case class Move(userId: UUID, x: Int, y: Int)
//  case class Action(userId: UUID)
//}

trait Actions {
  def userID: Int
}

class Join(val userID: Int) extends Actions

class ChooseClass(val userID: Int, clas: Class) extends Actions

class Move(val userID: Int, x: Int, y: Int) extends Actions

class Action(val userID: Int) extends Actions

object Actions {

  final case class CommandCreationException(message: String) extends Exception

  val MAX_X: Int  = 100
  val MAX_Y: Int  = 50
  val MIN_XY: Int = 0

  def getActionFromValue(actionString: String, userId: Int, clas: Option[String], x: Option[Int], y: Option[Int]): Either[CommandCreationException, Actions] =
    actionString match {
      case "join"        ⇒ Right(new Join(userId))
      case "chooseClass" ⇒ validateChooseClass(userId, clas)
      case "move"        ⇒ validateMovement(userId, x, y)
      case "action"      ⇒ Right(new Action(userId))
    }

  def validateChooseClass(userID: Int, clas: Option[String]): Either[CommandCreationException, ChooseClass] =
    clas
      .toRight("Class was not present")
      .flatMap { classValue ⇒
        val classOrError = Class.fromValue(classValue)
        classOrError.map(validatedClass ⇒ new ChooseClass(userID, validatedClass))
      }
      .leftMap(CommandCreationException)

  def validateMovement(userID: Int, x: Option[Int], y: Option[Int]): Either[CommandCreationException, Move] = {
    def validateCoord(value: Int, isX: Boolean): Option[Int] =
      if (isX)
        if (MIN_XY to MAX_X contains (value)) Some(value)
        else None
      else if (MIN_XY to MAX_Y contains (value)) Some(value)
      else None

    for {
      xCoord     ← x
      yCoord     ← y
      validatedX ← validateCoord(xCoord, isX = true)
      validatedY ← validateCoord(yCoord, isX = false)
    } yield new Move(userID, validatedX, validatedY)
  }.toRight(CommandCreationException("An error occurred when creating the movement command"))

}
