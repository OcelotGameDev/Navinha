extends Node2D

var timer = 0.0
export var time_betweeen_spawns = 1.0

func _ready():
	pass
	#timer = time_betweeen_spawns

func _process(delta):
	if timer < 0:
		timer = time_betweeen_spawns
		Spawn()
	else:
		timer -= delta
	
	
func Spawn():
	var enemy = load("res://Enemies/BasicEnemy.tscn")
	var instance = enemy.instance()
	get_tree().root.add_child(instance)
	instance.position = self.position + get_parent().position
