extends KinematicBody2D

var playerSpeed = 2
var velocidade = Vector2(0,0)
var vResultante = Vector2(0,0)

var bullet = preload("res://Bullet.tscn")
var canShoot = true
export var cadence = 0.2
export var bulletSpeed = 2

func _ready():
	pass

func _process(_delta):
	shoot()

func _physics_process(_delta):
	get_moveInput()
	move_and_collide(velocidade)

func get_moveInput():
	vResultante.x= Input.get_action_strength("Right") - Input.get_action_strength("Left")
	vResultante.y= -Input.get_action_strength("Up") - -Input.get_action_strength("Down")
	
	if vResultante != Vector2(0,0):
		velocidade = vResultante.normalized() * playerSpeed
	#fim do if	 
	
	else: 
		velocidade = Vector2 (0,0)
	#fim do else

func shoot():	
	if Input.is_action_pressed("Shoot") and canShoot:
		var spawnedBullet = bullet.instance()
		spawnedBullet.position = get_global_position()
		spawnedBullet.rotation = rotation_degrees
		spawnedBullet.apply_impulse(Vector2(0,0),Vector2(bulletSpeed,0).rotated(rotation))
		get_tree().get_root().add_child(spawnedBullet)
		canShoot = false
		yield (get_tree().create_timer(cadence), "timeout")	
		canShoot = true
	#fim do if	
