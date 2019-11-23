package com.nimbledinos.cowboyinterpreter

sealed abstract class Class

object Class {

  case class Fisher(value: String) extends Class

  case class Attacker(value: String) extends Class

  case class Defender(value: String) extends Class

  case class Builder(value: String) extends Class

  def fromValue(value: String): Either[String, Class] =
    value match {
      case "fisher"   ⇒ Right(Fisher(value))
      case "attacker" ⇒ Right(Attacker(value))
      case "defender" ⇒ Right(Defender(value))
      case "builder"  ⇒ Right(Builder(value))
      case _          ⇒ Left(s"${value} is not a valid class")
    }
}
