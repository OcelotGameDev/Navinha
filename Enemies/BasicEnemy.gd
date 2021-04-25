extends KinematicBody2D

export var move_direction = Vector2.LEFT
export var move_speed = 300

var movement_vector 

func _ready():
	movement_vector = move_direction * move_speed

func _process(delta):
	move_and_slide(movement_vector)
	
func _on_VisibilityNotifier2D_screen_exited():
	print(self.name," Deleted")
	queue_free()

func on_hit():
	print(self.name," Died")
	queue_free()
	
