package com.nimbledinos.cowboyinterpreter

import java.util.UUID

import cats.effect.IO
import com.nimbledinos.cowboyinterpreter.Actions.CommandCreationException
import com.twitter.finagle.{ Http, Service }
import com.twitter.finagle.http.{ Request, Response }
import com.twitter.util.Await
import io.circe.Json
import io.finch._
import io.finch.catsEffect._
import io.finch.circe._
import io.circe.generic.auto._
import io.circe.syntax._

import scala.collection.mutable
import scala.collection.mutable.Queue

object Main extends App {

  case class Message(hello: String)

  val commandQueue = mutable.Queue[Action]()

  def healthcheck: Endpoint[IO, String] = get(pathEmpty) {
    Ok("OK")
  }

  def commamd: Endpoint[IO, String] =
    get(
      "command" :: param("action") :: param[Int]("userId") :: paramOption("class") :: paramOption[Int]("x") :: paramOption[Int]("y")
    ) { (action: String, id: Int, clas: Option[String], x: Option[Int], y: Option[Int]) ⇒
      val createdAction: Either[CommandCreationException, Action] = Actions.getActionFromValue(action, id, clas, x, y)
      createdAction.fold(error ⇒ BadRequest(error), action ⇒ {
        commandQueue.enqueue(action);
        Ok("Action handled")
      })
    }

  def getCommand: Endpoint[IO, Json] = get("getCommand") {
    Ok(commandQueue.dequeue().asJson)
  }

  def helloWorld: Endpoint[IO, Message] = get("hello") {
    Ok(Message("World"))
  }

  def hello: Endpoint[IO, Message] = get("hello" :: path[String]) { s: String =>
    Ok(Message(s))
  }

  def service: Service[Request, Response] =
    Bootstrap
      .serve[Text.Plain](healthcheck :+: commamd)
      .serve[Application.Json](helloWorld :+: hello)
      .serve[Application.Json](getCommand)
      .toService

  Await.ready(Http.server.serve(":8081", service))
}
