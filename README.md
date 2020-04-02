#Eldritch Knight

#Next steps:
-Fix and animate block
-Convert animations to blend trees
-Better Attack animations

-implement menu and pause screens
-implement basic AI
-implement local coop
-implement first element
-acquire new weapons

#Project Details:
-3D Top down
-Medieval Fighting arena(at first)
-Skills similar to 9parchments, but conjures only on weapon
-Health and Mana as resources

#What is is about?
-conjuring elements into your equipment
-smiting foes with your sword magic
-quick-paced combat
-finding combos with martial and magical prowess alike

#Player Commands:
-LB - Dash(DONE)
-LT - Block
-RB - Light attack(should combo up to 3 times, with the first 2 stun-locking)
-RT - Heavy attack(only one swing)

-X - Conjure Cold
-Y - Conjure Fire
-B - Conjure Lightning
-A - Jump(DONE)

-LB + LT(during dash) - Shield Bash, knocks opponent back if not blocking
-LB + RB(during dash) - Quick thrust
-LB + RT(during dash) - Horizontal swing with knowckback

-LB(during jump) - Dash in the air(only once per jump)(DONE)
-LT(during jump) - Blocks and stops movement
-RB(during jump) - Horizontal circular slash(once per jump)
-RT(during jump) - Vertical plunge attack(falls to ground during attack)

-RT(hold) - launch attack

#Combos:(Hold element while acting)

Dashing has different effects:
-Cold causes damage to anything hit by dash
-Fire creates line of fire, causing DoT
-Lightning teleports instead

Block restores Mana if blocking the correct element

Light attacks cause different effects:
-Cold slows and freezes on third hit within 1 second of each other
-Fire attacks with increased range as an arc
-Lightning shoots projectiles forward

Heavy attacks cause explosion
-Cold creates wall in a line
-Fire causes heaviest damage
-Lightning knocks back and shoots projectiles in the 8 directions
