extends KinematicBody2D

var Speed = Vector2(0,0)

func _ready():
	Speed.x=50

func _physics_process(delta):
	if position.x > 1000:
		queue_free()
	
	move_and_collide(Speed)
