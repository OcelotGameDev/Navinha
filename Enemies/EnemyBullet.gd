extends Node

export var speed = 0


func _ready():
	pass

func _physics_process(delta):
	pass

func _on_Area2D_body_entered(body):
	if body.has_method("on_hit"):
		body.on_hit()
