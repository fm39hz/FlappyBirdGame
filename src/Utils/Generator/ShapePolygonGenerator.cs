namespace FlappyBirdGame.Utils.Generator;

using Godot.Collections;

public static class ShapePolygonGenerator {
	private const float ACCURACY = 0.42f;

	private static Vector2I GetSpriteInformation(this Sprite2D spriteSheet) {
		var texture = spriteSheet.Texture;
		var width = texture.GetWidth() / spriteSheet.Hframes;
		var height = texture.GetHeight() / spriteSheet.Vframes;
		return new Vector2I(width, height);
	}

	public static Dictionary<int, CollisionPolygon2D> GetArea(this Sprite2D spriteSheet, Bitmap bitmap, string name) {
		var shapePool = new Dictionary<int, CollisionPolygon2D>();
		var texture = spriteSheet.Texture;
		var (width, height) = spriteSheet.GetSpriteInformation();
		for (int frame = 0, state = 0; frame < spriteSheet.Hframes * spriteSheet.Vframes; frame++) {
			var position = new Vector2I {
				X = frame * width - texture.GetWidth() * state, Y = state * height
			};
			var polys = bitmap.OpaqueToPolygons(new Rect2I(position, width, height), ACCURACY);
			foreach (var shape in polys) {
				shapePool.Add(frame, ConfigPolygon(shape, spriteSheet.Position, name + "_" + frame));
			}

			if (frame == spriteSheet.Hframes * (state + 1) - 1) {
				state++;
			}
		}

		return shapePool;
	}

	private static CollisionPolygon2D ConfigPolygon(Vector2[] poly, Vector2 position, string name) {
		for (var i = 0; i < poly.Length; i++) {
			poly[i] += position;
		}

		var shape = new CollisionPolygon2D {
			Polygon = poly, Name = name
		};
		return shape;
	}
}
